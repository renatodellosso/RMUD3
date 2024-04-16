const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: ["./client/public/main.css", "./client/lib/index.ts"],
    output: {
        path: path.resolve(__dirname, "client", "public", "dist"),
        filename: "[name].js",
        publicPath: "/",
    },
    resolve: {
        extensions: [".js", ".ts", ".css"],
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: "ts-loader",
            },
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, "css-loader", "postcss-loader"],
            },
        ],
    },
    plugins: [
        new CleanWebpackPlugin(),
        //new HtmlWebpackPlugin({
        //    template: "./client/public/index.html",
        //}),
        new MiniCssExtractPlugin({
            filename: "[name].css",
        }),
    ],
};