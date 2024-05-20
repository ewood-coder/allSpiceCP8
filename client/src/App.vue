<script setup>
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'


</script>

<template>
	<header>
		<Navbar />
	</header>
	<main class="fontPage">
		<router-view />
	</main>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";
@import url('https://fonts.googleapis.com/css2?family=Sahitya:wght@400;700&display=swap');

:root {
	--main-height: calc(100vh - 32px - 64px);
}


footer {
	display: grid;
	place-content: center;
	height: 32px;
}

.fontPage {
	font-family: "Sahitya", serif;
}
</style>
