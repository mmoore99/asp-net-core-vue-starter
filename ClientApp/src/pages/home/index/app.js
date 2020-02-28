import Vue from "vue";
//Vue.config.devtools = true;
Vue.config.productionTip = false;

window.fb = {
  vueApp: null,

  initializePage() {
    this.vueApp = this.initializeVue();
    console.log(window.fb);
  },

  initializeVue() {
    const vue = new Vue({
      el: "#app",
      data: {
      },
      created() {
      },
      mounted() {

      },
      methods: {
      },
      computed: {
      }
    });
    return vue;
  }
};
