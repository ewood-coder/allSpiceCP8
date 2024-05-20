<script setup>
import { Recipe } from '../models/Recipe.js';

defineProps({
	recipe: { type: Recipe, required: true }
})
</script>


<template>
	<div class="recipe-card rounded-4">
		<div class="px-2 py-1">{{ recipe.category }}</div>
		<img :src="recipe.img" :alt="recipe.title" class="recipe-img">
		<div class="px-4 py-2">
			<div class="fs-3">{{ recipe.title }}</div>
			<!-- FIXME truncate text -->
			<!-- <div class="text-end">
				<RouterLink :to="{ name: 'recipe Details', params: { recipeId: recipe.id } }">
					<button class="btn btn-danger">See More</button>
				</RouterLink>
			</div> -->
		</div>
	</div>
</template>


<style scoped>
.recipe-card {
	background-color: #3F3B3B;
	color: white;

	width: auto;
	height: 100%;
}

.profile-picture {
	height: 10vh;
	aspect-ratio: 1/1;
	border-radius: 50%;
}

.recipe-img {
	width: 100%;
	height: 40vh;
	object-fit: cover;

}
</style>