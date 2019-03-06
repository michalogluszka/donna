<template>
  <div class="hello">
    <b-button variant="danger">Text</b-button>
    <h3>{{ msg }}</h3>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class DonnaMain extends Vue {
  @Prop() private msg!: string;
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}

div.hello {
  color: darkgreen;
}
</style>
