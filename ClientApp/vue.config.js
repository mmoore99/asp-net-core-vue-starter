"use strict";
const glob = require("glob");
const fs = require("fs");
const pages = {};
glob.sync("./src/pages/**/").forEach(path => {
    const lastFolderName = path.match(/([^\/]*)\/*$/)[1];
    const pathToCheck = `${path}${lastFolderName}.js`;
    if (!fs.existsSync(pathToCheck)) {
        console.log(`file does not exist: ${pathToCheck}`);
        return;
    }
    console.log(`file exists, processing as page entry point: ${pathToCheck}`);
    const pathTokens = pathToCheck.split("/");
    const chunk = `${pathTokens[3]}/${lastFolderName}`;
    pages[chunk] = {
        entry: pathToCheck,
        template: "public/index.html",
        //title: titles[chunk],
        chunks: ["chunk-vendors", "chunk-common", chunk]
    };
});
console.log(pages);

module.exports = {
    // to force inclusion of runtime compiler: either use "runtimeCompiler: true" in vue.config.js
    // or import "Vue from 'vue/dist/vue.js'" instead of "import Vue from 'vue'" in app.js
    //see https://github.com/vuejs-templates/webpack/issues/215
    runtimeCompiler: true,
    pages,
    outputDir: "../wwwroot/dist/",
    publicPath: "/dist/",
    filenameHashing: false,
    lintOnSave: true,
    devServer: {
        //progress: false
    
    },

    configureWebpack:  {

        devServer: {
            stats: 'verbose',

            // See the following warning when using a string in the proxy option
            //     https://cli.vuejs.org/config/#devserver-proxy
            //     When devServer.proxy is set to a string, only XHR requests will be proxied.
            //     If you want to test an API URL, don't open it in the browser, use an API tool like Postman instead.
            // proxy: process.env.ASPNET_URL || 'https://localhost:44345'

            // using a proxy with object to avoid issue discussed above
            // When running in IISExpress, the env variable wont be provided. Hard code it here based on your launchSettings.json
            proxy: {
                '^/': {
                    target: process.env.ASPNET_URL || 'https://localhost:44345'
                    //target: 'https://localhost:5001'
                }
            }
        },


        // Use source map for debugging in VS and VS Code
        // devtool: 'source-map',
        // Breakpoints in VS and VSCode wont work since the source maps consider client-app the project root, rather than its parent folder
        output: {
            devtoolModuleFilenameTemplate: info => {
                const resourcePath = info.resourcePath.replace(
                    "./src",
                    "./ClientApp/src"
                );
                return `webpack:///${resourcePath}?${info.loaders}`;
            }
        }
    },

    //following disables output of html
    chainWebpack: config => {
        Object.keys(pages).forEach(page => {
            config.plugins.delete(`html-${page}`);
            config.plugins.delete(`preload-${page}`);
            config.plugins.delete(`prefetch-${page}`);
        });
    }
    
};
