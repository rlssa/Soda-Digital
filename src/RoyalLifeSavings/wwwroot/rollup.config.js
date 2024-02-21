import typescript from "@rollup/plugin-typescript";
import commonjs from "@rollup/plugin-commonjs";
import resolve from "@rollup/plugin-node-resolve";
import styles from "rollup-plugin-styles";
import { terser } from "rollup-plugin-terser";
import svelte from "rollup-plugin-svelte";
import sveltePreprocess from "svelte-preprocess";

const production = !process.env.ROLLUP_WATCH;

export default [
    {
        input: "./Client/index.ts",
        output: {
            sourcemap: true,
            format: "esm",
            dir: "./wwwroot/dist",
            entryFileNames: "[name].js",
            chunkFileNames: "[name].[hash].js",
            assetFileNames: "[name][extname]",
        },
        preserveEntrySignatures: false,
        plugins: [
            svelte({
                preprocess: sveltePreprocess({}),
                // You can restrict which files are compiled
                // using `include` and `exclude`
                include: "./Client/components/**/*.svelte",

                // Emit CSS as "files" for other plugins to process. default is true
                emitCss: true,
            }),
            styles({
                sourceMap: true,
                url: {
                    publicPath: "./assets",
                },
                mode: "extract",
            }),

            resolve({
                browser: true,
            }),
            commonjs(),
            typescript({ sourceMap: !production }),
            production && terser(),
        ],
    },
];