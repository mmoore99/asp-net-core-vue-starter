module.exports = {
  root: true,
  env: {
    node: true
  },
  'extends': [
    'plugin:vue/essential',
    '@vue/standard'
  ],
  rules: {
    'no-console': 0,
    //'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    //'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'no-debugger': 1,
    "semi": [2, "always"],
    "indent": 0,
    "space-before-function-paren": [2, "never"],
    "no-unused-vars": 0,
    "quotes": [1, "double"],
    "no-trailing-spaces": 0,
    "padded-blocks": 0,
    "spaced-comment": 0
  },
  parserOptions: {
    parser: 'babel-eslint'
  }
}
