import Vue from "vue";
import SampleApi from "./SampleApi.vue";
Vue.config.productionTip = false;

window.fb = {
    vueApp: null,

    initializePage() {
        this.vueApp = this.initializeVue();
    },

    initializeVue() {
        const vue = new Vue({
            el: "#app",
            components: {
                SampleApi
            },
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
