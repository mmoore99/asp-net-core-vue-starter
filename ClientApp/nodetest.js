const glob = require("glob");
const fs = require('fs');
const pages = {};
//glob.sync("./src/pages/**/app.js").forEach(path => {
glob.sync("./src/pages/**/").forEach(path => {
    //console.log(`path=${path}`);
    const lastFolderName = path.match(/([^\/]*)\/*$/)[1];
    //console.log(`lastFolderName=${lastFolderName}`);
    const pathToCheck = `${path}${lastFolderName}.js`;
    //console.log(`pathToCheck=${pathToCheck}`);
    if (!fs.existsSync(pathToCheck)) {
        console.log(`file does not exist: ${pathToCheck}`);
        return;
    }
    console.log(`file exists, processing as page entry point: ${pathToCheck}`);
    //debugger;
    //const chunk = path.split("./src/pages/")[1].split("/app.js")[0];
    const pathTokens = pathToCheck.split("/");
    const chunk = `${pathTokens[3]}/${lastFolderName}`;
    console.log(`chunk=${chunk}`);
    pages[chunk] = {
        entry: path,
        template: "public/index.html",
        //title: titles[chunk],
        chunks: ["chunk-vendors", "chunk-common", chunk]
    };
});
console.log(pages);
