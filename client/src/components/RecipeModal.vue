<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Recipe } from '../models/Recipe.js';
import { Ingredient } from '../models/Ingredient.js';

const recipe = computed(() => AppState.activeRecipe)
const ingredient = computed(() => AppState.ingredients)

</script>


<template>
	<div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel" aria-hidden="true">
		<div class="modal-dialog modal-xl">
			<div v-if="recipe" class="modal-content">

				<div class="modal-header">
					<h1 class="modal-title fs-5" id="recipeModalLabel">{{ recipe.title }}</h1>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>

				<div class="modal-body">
					<div class="container-fluid">
						<section class="row">

							<img :src="recipe.img" :alt="`image of ${recipe.title}`" class="col-6">

							<!-- <div class="col-6">{{ ingredient.recipeId }}</div> -->

						</section>
					</div>
				</div>

				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
				</div>

			</div>
		</div>
	</div>
</template>


<style scoped>
img {
	width: 100%;
	margin-bottom: 1em;
}
</style>