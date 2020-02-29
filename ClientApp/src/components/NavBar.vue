<template>
    <div id="nav">
        <a href="/Home/Index">Home</a>  |
        <a href="/Home/SampleApi">Sample API</a>  |
        <a href="/Home/About">About</a>
    </div>
</template>

<script>

    // @ is an alias to /src
    //import HelloWorld from '@/components/HelloWorld.vue'

    export default {
        name: "NavBar",
        props: {

        },
        components: {

        },
        data() {
            return {

            };
        },
        created() {

        },
        mounted() {

        },
        methods: {

        },

        computed: {

        }
    };

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  #nav {
    padding: 30px;
  }

    #nav a {
      font-weight: bold;
      color: #2c3e50;
    }

      #nav a.router-link-exact-active {
        color: #42b983;
      }
</style>
