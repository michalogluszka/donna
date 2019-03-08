<template>
  <div class="hello">
    <h3>{{ welcomeMessage }}</h3>
    <p>{{ messageList }}</p>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import Axios from 'axios';

@Component
export default class DonnaMain extends Vue {

  @Prop() private welcomeMessage!: string;

  private messageList = '';

  private mounted() {
    this.messageList += this.getTranslation();
  }

  private getTranslation() {
    return Axios({ method: 'GET', url: 'https://httpbin.org/ip' }).then(
      (result) => {
        return result.statusText;
      },
      (error) => {
        return error;
      },
    );
  }
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
