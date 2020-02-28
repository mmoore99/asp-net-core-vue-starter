"use strict";
//const titles = require("./title.js");
const glob = require("glob");
const pages = {};

glob.sync("./src/pages/**/app.js").forEach(path => {
  const chunk = path.split("./src/pages/")[1].split("/app.js")[0];
  console.log(chunk);
  pages[chunk] = {
    entry: path,
    template: "public/index.html",
    //title: titles[chunk],
    chunks: ["chunk-vendors", "chunk-common", chunk]
  };
});

module.exports = {
  // to force inclusion of runtime compiler: either use "runtimeCompiler: true" in vue.config.js
  // or import "Vue from 'vue/dist/vue.js'" instead of "import Vue from 'vue'" in app.js
  //see https://github.com/vuejs-templates/webpack/issues/215
  runtimeCompiler: true,
  pages,
  outputDir: "../wwwroot/dist/",
  filenameHashing: false,
  devServer: {
    //progress: false
  },

  configureWebpack: config => {

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
      };
    }
  }
};
