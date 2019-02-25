<template>
  <div id="app">    
    <DonnaMain msg="Hi Michal"/>    
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from './components/HelloWorld.vue';
import DonnaMain from './components/DonnaMain.vue';

@Component({
  components: {
    DonnaMain,
  },
})

export default class App extends Vue {}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: left;  
  color: #2c3e50;
  margin: 0px;
}
</style>
