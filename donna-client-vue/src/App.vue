<template>
  <div id="app" class="app main-page d-flex flex-column flex-fill">    
    <DonnaMain msg="Hi Michal"/>
  </div>
</template>

<script lang="ts">

import { Component, Vue } from 'vue-property-decorator';
import BootstrapVue from 'bootstrap-vue';
import HelloWorld from './components/HelloWorld.vue';
import DonnaMain from './components/DonnaMain.vue';

Vue.use(BootstrapVue);

@Component({
  components: {
    DonnaMain,
  },
})

export default class App extends Vue {}
</script>

<style>
div.app {
  min-height: 100vh;

  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: left;  
  color: #2c3e50;
  margin: 0px;
  padding:0px; 

  background-color: white;
  background-image: url('./assets/donna_background.jpg');
  background-position: center;
  background-repeat: no-repeat;
  /*background-size: cover;*/
}
</style>
