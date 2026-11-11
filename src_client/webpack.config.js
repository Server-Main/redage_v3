const path = require("path");

const LIBRARY_NAME = 'RAGECODE.PRO-clientside';
const OUTPUT_FILE = 'main.js';

module.exports = mode => ({
    entry: './index.js',
    mode,
    optimization: {
        minimize: true
    },
    performance: {
        hints: false
    },
    output: {
        path: path.join(__dirname, "../client_packages"),
        filename: OUTPUT_FILE,
        library: LIBRARY_NAME,
        libraryTarget: 'umd',
        globalObject: 'global',
    }
});