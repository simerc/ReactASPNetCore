const webpack = require('webpack');
const path = require ('path');

const merge = require('webpack-merge');
const baseConfig = require('./webpack.base.js');

const APP_DIR = path.resolve(__dirname,'ClientApp');
const PUBLIC_DIR = path.resolve(__dirname,'public');

const config = {

    entry: APP_DIR + '/Client.js',
    devServer:{
        contentBase: PUBLIC_DIR,
        port: 9000,
        open: true,
        historyApiFallback: true
    },
    output: {
        path: PUBLIC_DIR,
        filename: 'clientbundle.js'
    }
    
};

module.exports = merge(baseConfig, config);