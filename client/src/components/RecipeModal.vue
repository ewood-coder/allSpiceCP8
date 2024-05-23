<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Recipe } from '../models/Recipe.js';
import { Ingredient } from '../models/Ingredient.js';
import { favoritesService } from '../services/FavoritesService.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { ingredientService } from '../services/IngredientsService.js';

const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.activeIngredients)
const account = computed(() => AppState.account)
const isFavorited = computed(() => AppState.favorites.find(fav => fav.id === recipe.value?.id))
const isCreator = computed(() => AppState.account?.id === recipe.value?.creatorId)

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
async function saveInstructions(event) {
	const form = event.target
	const instructions = form.instructions.value
	AppState.activeRecipe.instructions = instructions
	try {
		recipesService.updateRecipe(AppState.activeRecipe.id, AppState.activeRecipe)
		Pop.success('Instructions Updated')
	}
	catch (error) {
		Pop.error(error)
	}
}

async function deleteIngredient(ingredientId) {
	try {
		const res = await Pop.confirm('Are you sure you want to delete this ingredient?')
		if (!res) {
			return
		}
		await ingredientService.deleteIngredient(ingredientId)
		Pop.success('Ingredient Deleted')
	}
	catch (error) {
		Pop.error(error)
	}
}

async function addIngredient(event) {
	const form = event.target
	const ingredient = {
		name: form.name.value,
		quantity: form.quantity.value,
		recipeId: AppState.activeRecipe.id
	}
	try {
		await ingredientService.createIngredient(ingredient)
		Pop.success('Ingredient Added')
		form.reset()
	}
	catch (error) {
		Pop.error(error)
	}
}

async function deleteRecipe() {
	try {
		const res = await Pop.confirm('Are you sure you want to delete this recipe?')
		if (!res) {
			return
		}
		await recipesService.deleteRecipe(AppState.activeRecipe.id)
		Pop.success('Recipe Deleted')
	}
	catch (error) {
		Pop.error(error)
	}
}
// ------------------------------goober-----------------------



</script>


<template>
	<div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel" aria-hidden="true">
		<div class="modal-dialog modal-xl">
			<div class="modal-content">

				<div class="modal-header">
					<h1 class="modal-title fs-5 my-auto text-capitalize" id="recipeModalLabel">
						{{ recipe?.title }} &nbsp;>&nbsp; {{ recipe?.category }}
					</h1>

					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>

				<div class="modal-body">
					<div class="container-fluid">
						<section class="row">

							<div class="col-12 col-lg-6">
								<img :src="recipe?.img" :alt="`image of ${recipe?.title}`">
							</div>

							<div class="col-12 col-lg-6">

								<div>
									<h5 class="text-decoration-underline">
										<b>Instructions:</b>
									</h5>
									<div>
										<form action="" @submit.prevent="saveInstructions">
											<textarea class="form-control" rows="5" :readonly="!isCreator"
												:value="recipe?.instructions" name="instructions" id="instructions">
											</textarea>

											<button v-if="isCreator" class="btn btn-primary mt-3 d-flex mx-auto">Save
												Changes</button>
										</form>
									</div>
								</div>

								<hr class="my-3" />

								<div class="mb-5">
									<h5 class="text-decoration-underline">
										<b>Ingredients:</b>
									</h5>
									<ul class="flex gap-2">
										<li v-for="ingredient in ingredients" :key="ingredient.id">
											<span>
												{{ ingredient.name }} &nbsp;|&nbsp; Qty: {{ ingredient.quantity }}
											</span>
											<button v-if="isCreator" @click="deleteIngredient(ingredient.id)"
												class="btn btn-danger p-1 py-0 ms-3">
												<i class="mdi mdi-trash-can-outline d-flex align-content-center fs-5"></i>
											</button>
										</li>
									</ul>
								</div>


								<form v-if="isCreator" action="" class="d-flex gap-2 align-items-center container-fluid"
									@submit.prevent="addIngredient">
									<div class="row justify-content-between align-items-center">
										<div class="mb-3 col-12 col-md-5">
											<label for="name" class="form-label">Name:</label>
											<input type="text" required class="form-control" id="name" name="name">
										</div>
										<div class="mb-3 my-auto col-12 col-md-5">
											<label for="quantity" class="form-label col-12">Quantity:</label>
											<input type="number" required class="form-control" id="quantity" name="quantity">
										</div>
										<button
											class="btn btn-primary mt-3 d-flex mx-auto fs-5 py-1 align-items-center justify-content-center col-2 col-md-1">
											<i class="mdi mdi-plus"></i>
										</button>
									</div>
								</form>

							</div>

						</section>
					</div>
				</div>

				<div class="modal-footer justify-content-between">
					<div class="d-flex gap-2 align-items-center">

						<img :src="recipe?.creator.picture" alt="" class="profileImg">
						<span class="mb-3">
							{{ recipe?.creator.name }}
						</span>

					</div>


					<div class="d-flex">

						<span class="pe-4">
							<div v-if="account == null">
								<i class="mdi mdi-heart-off-outline fs-1 rounded-4 btnLikeOff px-2" title="Login Required"></i>
							</div>

							<div v-else>
								<button v-if="!isFavorited" @click="FavoriteRecipe(recipe?.id)" class="btnLike rounded-4"
									:title="`Favorite this Recipe`">
									<i class="mdi mdi-heart-outline fs-1"></i>
								</button>
								<button v-if="isFavorited" @click="RemoveFavoriteRecipe(isFavorited.favoriteId)"
									class="btnAlreadyLiked rounded-4" :title="`Unfavorite this Recipe`">
									<i class="mdi mdi-heart fs-1"></i>
								</button>
							</div>
						</span>

						<span v-if="isCreator" class="d-flex align-items-center">
							<button class="btn btn-danger" data-bs-dismiss="modal" @click="deleteRecipe">Delete</button>
						</span>

						<!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> -->
					</div>

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


.btnLike {
	color: rgb(0, 0, 0);
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnLike:hover {
	transition: all 0.3s ease-in-out;
	color: #ff3f92;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnAlreadyLiked {
	/* color: #e44b7e; */
	/* color: #0084ff; */
	color: #ff3f92;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.btnLikeOff {
	color: white;
	background-color: rgba(255, 255, 255, 0);
	border: none;
}

.profileImg {
	width: 50px;
	height: 50px;
	border-radius: 50%;
}
</style>