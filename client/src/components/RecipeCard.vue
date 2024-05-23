<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { Recipe } from '../models/Recipe.js';
import { AppState } from '../AppState.js';
import { favoritesService } from '../services/FavoritesService.js';
import { Favorite } from '../models/Favorite.js';
import { recipesService } from '../services/RecipesService.js';


const account = computed(() => AppState.account)
const favorites = computed(() => AppState.favorites)
const recipes = computed(() => AppState.recipes)
const isFavorited = computed(() => AppState.favorites.find(favorite => favorite.recipeId == props.recipe.id))


const props = defineProps({
	recipe: { type: Recipe, required: true },
})


// STUB: FUNCTIONS: -----------------------------------
async function FavoriteRecipe(recipeId) {
	try {
		await favoritesService.FavoriteRecipe(recipeId)
	} catch (error) {
		Pop.error(error)
	}
}

async function RemoveFavoriteRecipe(favoriteId) {
	try {
		await favoritesService.RemoveFavoriteRecipe(favoriteId)
	} catch (error) {
		Pop.error(error)
	}
}

async function setActiveRecipe() {
	console.log('setting active recipe', props.recipe)
	await recipesService.setActiveRecipe(props.recipe)
}

// ---------------------------------------------------

</script>


<template>
	<div class="recipe-card rounded-4 mask1">
		<div class="d-flex justify-content-between align-items-center px-2">

			<div class="px-2 py-1 fontSize text-capitalize">{{ recipe.category }}</div>

			{{ recipe.id }}

			<div v-if="account == null">
				<i class="mdi mdi-heart-off-outline fs-3 rounded-4 btnLikeOff px-2" title="Login Required"></i>
			</div>

			<div v-else>
				<button v-if="!isFavorited" @click="FavoriteRecipe(recipe.id)" class="btnLike rounded-4"
					:title="`Favorite this Recipe`">
					<i class="mdi mdi-heart-outline fs-3"></i>
				</button>

				<button v-if="isFavorited" @click="RemoveFavoriteRecipe(isFavorited.favoriteId)"
					class="btnAlreadyLiked rounded-4" :title="`Unfavorite this Recipe`">
					<i class="mdi mdi-heart fs-3"></i>
				</button>
			</div>
		</div>

		<img :src="recipe.img" :alt="recipe.title" data-bs-toggle="modal" data-bs-target="#recipeModal" role="button"
			@click="setActiveRecipe()" :title="`recipe information for ${recipe.title}`" class="recipe-img">
		<div class="px-4 py-2 bgColor">
			<div class="fs-3">{{ recipe.title }}</div>
		</div>
	</div>
</template>


<style scoped>
.recipe-card {
	/* background-color: #ff9074; */
	background-color: #fdac98;

	color: white;

	width: auto;
	height: 100%;

	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	/* box-shadow: rgba(50, 50, 93, 0.25) 0px 2px 5px -1px, rgba(0, 0, 0, 0.3) 0px 1px 3px -1px; */
}

.recipe-img {
	width: 100%;
	height: 40vh;
	object-fit: cover;
}

.fontSize {
	font-size: 20px;
}


.btnLike {
	color: white;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnLike:hover {
	transition: all 0.3s ease-in-out;
	color: #ff0000;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnAlreadyLiked {
	/* color: #e44b7e; */
	/* color: #0084ff; */
	color: #ff0000;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnLikeOff {
	color: white;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.filterBox {
	background-color: rgba(165, 165, 165, 0.5);
	backdrop-filter: blur(15px);
	border-radius: 15px;

	display: flex;
	justify-content: center;
	align-items: center;
}

.floatText {
	position: absolute;
}

.mask1 {
	text-shadow: 1px 1px 1px rgb(0, 0, 0);
}
</style>