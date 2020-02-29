import Vue from "vue";
import NavBar from "@/components/NavBar.vue";
import HelloWorld from "./HelloWorld.vue";
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
                NavBar,
                HelloWorld
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
