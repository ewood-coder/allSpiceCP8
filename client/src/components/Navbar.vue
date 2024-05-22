<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
	document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
	theme.value = theme.value == 'light' ? 'dark' : 'light'
	document.documentElement.setAttribute('data-bs-theme', theme.value)
	saveState('theme', theme.value)
}

</script>

<template>
	<nav class="navbar navbar-expand-sm navbar-light bg-light px-3 py-md-4 navbarBoxShadow">
		<router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
			<div class="d-flex flex-column align-items-center">
				<img alt="logo" src="../assets/img/logo2noBG.png" height="65" />
			</div>
		</router-link>
		<button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
			aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse" id="navbarText">
			<div class="ms-0 ms-md-auto d-flex me-auto me-md-0 text-end">
				<!-- LOGIN COMPONENT HERE -->
				<Login class="pe-3 d-flex" />

				<button class="btn text-dark fs-3 align-content-center hidden" @click="toggleTheme"
					:title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
					<i class="mdi" :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i>
				</button>
			</div>
		</div>
	</nav>
</template>

<style scoped>
.hidden {
	display: none;
}

a:hover {
	text-decoration: none;
}

.nav-link {
	text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
	border-bottom: 2px solid var(--bs-success);
	border-bottom-left-radius: 0;
	border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
	nav {
		height: 64px;
	}
}


.navbarBoxShadow {
	box-shadow: 0px 0px 5px -3px rgba(0, 0, 0, 0.5);
}
</style>
