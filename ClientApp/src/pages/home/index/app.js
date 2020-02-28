import Vue from "vue";
var fb = window.fb;
//Vue.config.devtools = true;
Vue.config.productionTip = false;

//const vm = new Vue({
//  el: "#app",
//  data: {

//  },
//  created() {
//    debugger;
//  },
//  mounted() {

//  },
//  methods: {

//  },
//  computed: {

//  }
//});
//debugger;

//fb.module = (function() {
//  var _init = function() {
//    const vm = new Vue({
//      el: "#app",
//      data: {

//      },
//      created() {
//        debugger;
//        console.log("in created");
//      },
//      mounted() {

//      },
//      methods: {
        
//      },
//      computed: {
        
//      }
//    });
//    debugger;
//  };

//  return {
//    init: _init
//  };
//})();
fb.vue = null;
fb.initializePage = function(urls) {
  fb.vue = fb.initializeVue();
};

fb.initializeVue = function() {
  const vm = new Vue({
    el: "#app",
    data: {
    },
    created() {
      debugger;
      console.log("in created");
    },
    mounted() {

    },
    methods: {
    },
    computed: {
    }
  });
  return vm;
};


