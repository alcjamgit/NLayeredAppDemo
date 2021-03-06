﻿"use strict";

module.exports = {
    entry: "./index.jsx",
    output: {
        filename: "bundle.js"
    },
    module: {
        loaders: [
            {
                test: /\.jsx$/,
                loader: "babel-loader",
                exclude: /node_modules/,
                query: {
                    presets: ["es2015", "react"]
                }
            }
        ]
    },
    resolve: {
        extensions: ['', '.js', '.jsx'],
    }  
};