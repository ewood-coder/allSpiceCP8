<script setup>
import { computed, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { Recipe } from '../models/Recipe.js';
import { recipesService } from '../services/RecipesService.js';
import { Account } from '../models/Account.js';
import RecipeCard from '../components/RecipeCard.vue';



const filterBy = ref('all')

const recipes = computed(() => {
	if (filterBy.value == 'all') return AppState.recipes
	return AppState.recipes.filter(event => event.category == filterBy.value)
})

const filters = [
	{
		name: 'all',
		backgroundImage: 'https://i.pinimg.com/736x/b6/18/60/b61860319943ef8f5062b08b92e959ab.jpg'
	},
	{
		name: 'breakfast',
		backgroundImage: 'https://images.unsplash.com/photo-1482049016688-2d3e1b311543?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJyZWFrZmFzdHxlbnwwfHwwfHx8MA%3D%3D',
	},
	{
		name: 'lunch',
		backgroundImage: 'https://plus.unsplash.com/premium_photo-1672242676674-f4349cc6470e?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bHVuY2h8ZW58MHx8MHx8fDA%3D',
	},
	{
		name: 'dinner',
		backgroundImage: 'https://plus.unsplash.com/premium_photo-1677000666741-17c3c57139a2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NTN8fGRpbm5lcnxlbnwwfHwwfHx8MA%3D%3D'
	},
	{
		name: 'snack',
		backgroundImage: 'https://images.unsplash.com/photo-1604307078172-9b46710cc5af?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fHNuYWNrfGVufDB8fDB8fHww'
	},
	{
		name: 'dessert',
		backgroundImage: 'https://images.unsplash.com/photo-1551024506-0bccd828d307?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8ZGVzc2VydHxlbnwwfHwwfHx8MA%3D%3D'
	}
]

async function getRecipes() {
	try {
		await recipesService.getRecipes()
	} catch (error) {
		Pop.error(error)
	}
}

onMounted(() => {
	getRecipes()
})

</script>

<template>

	<!-- SECTION: JUMBOTRON -->
	<section class="container-fluid p-3">
		<div class="p-5 text-center bg-image jumbotron rounded-4">
			<div class="d-flex justify-content-center align-items-center h-100">
				<div class="text-white">
					<h1 class="mb-3 mask">All-Spice</h1>
					<h4 class="mb-3 mask">Cherish Your Family And Their Cooking</h4>
				</div>
				<div class="d-flex justify-content-around align-items-center homeOptions py-1">
					<span>
						<button class="btnColor btn fs-custom">Home</button>
					</span>

					<span>
						<button class="btnColor btn fs-custom">My Recipes</button>
					</span>

					<span>
						<button class="btnColor btn fs-custom">Favorites</button>
					</span>
				</div>
			</div>
		</div>
	</section>
	<!-- Jumbotron -->


	<div class="container-fluid">
		<!-- SECTION: FILTER ACCORDION -->
		<section class="row my-5 pt-3 text-center d-flex justify-content-center">

			<div class="accordion accordion-item col-10 accordionContainer" id="accordion2">
				<h2 class="accordion-header">

					<button class="accordion-button collapsed text-center" type="button" data-bs-toggle="collapse"
						data-bs-target="#flush-collapseTwo" aria-expanded="false" aria-controls="flush-collapseTwo">

						<div>
							<h4 class="text-center mb-0">Explore Food Categories:</h4>
						</div>
					</button>
				</h2>

				<div id="flush-collapseTwo" class="accordion-collapse collapse" data-bs-parent="#accordion2">
					<div class="accordion-body d-flex flex-wrap px-0">

						<div class="col-12 col-md-6 text-center fs-3 p-2" v-for="filterObj in filters" :key="filterObj.name">
							<div @click="filterBy = filterObj.name" role="button" class="filter-card rounded selectable mask"
								:style="`--bg-img: url(${filterObj.backgroundImage})`">
								<div class="filterBox px-3">
									{{ filterObj.name }}
								</div>
							</div>
						</div>

					</div>
				</div>
			</div>

		</section>


		<hr class="my-5" />

		<!-- SECTION: RECIPES -->
		<!-- <h2 class="mb-4 text-center"><u>Recipes</u></h2> -->
		<section class="d-flex flex-wrap justify-content-center row">
			<div v-for="recipe in recipes" :key="recipe.id" class="col-11 col-md-6 col-lg-4 mb-4 px-0 px-md-4 py-2">

				<RecipeCard :recipe="recipe" />

			</div>
		</section>
	</div>

	<!-- NOTE the modal has to be here, it's just hidden from view -->
	<!-- <ModalWrapper modalId="eventFormModal">
		<EventForm />
	</ModalWrapper> -->
</template>

<style scoped lang="scss">
.filter-card {
	height: 10vh;
	// background-color: purple;
	display: flex;
	justify-content: center;
	align-items: center;
	background-image: var(--bg-img);
	background-position: center;
	background-size: cover;
	color: white;
	font-weight: bold;
	text-shadow: 0px 0px 10px black;
	filter: contrast(.8);
}

.homeOptions {
	border: solid 5px white;
	background-color: white;
	border-radius: 5px;
	width: 30em;

	position: absolute;
	top: 18em;
	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
}

.fs-custom {
	font-size: 20px;
}

// SECTION: MOBILE STYES FOR homeOptions BAR --------------------------

@media only screen and (max-width: 374px) {
	.homeOptions {
		border: solid 5px white;
		background-color: white;
		border-radius: 5px;
		width: 16em;
		height: 4em;

		position: absolute;
		top: 19.7em;
		box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	}

	.btnColor {
		color: #219653;
		// font-weight: bold;
		font-family: "Sahitya", serif;
	}

	.fs-custom {
		font-size: 15px;
	}
}

@media (min-width: 375px) and (max-width: 424px) {
	.homeOptions {
		border: solid 5px white;
		background-color: white;
		border-radius: 5px;
		width: 18em;
		height: 4em;

		position: absolute;
		top: 19.7em;
		box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	}

	.btnColor {
		color: #219653;
		// font-weight: bold;
		font-family: "Sahitya", serif;
	}

	.fs-custom {
		font-size: 15px;
	}
}

@media (min-width: 425px) and (max-width: 575px) {
	.homeOptions {
		border: solid 5px white;
		background-color: white;
		border-radius: 5px;
		width: 21em;
		height: 4em;

		position: absolute;
		top: 19.7em;
		box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	}

	.btnColor {
		color: #219653;
		// font-weight: bold;
		font-family: "Sahitya", serif;
	}

	.fs-custom {
		font-size: 15px;
	}
}

@media (min-width: 576px) and (max-width: 767px) {
	.homeOptions {
		border: solid 5px white;
		background-color: white;
		border-radius: 5px;
		width: 25em;
		height: 4em;

		position: absolute;
		top: 18em;
		box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	}

	.btnColor {
		color: #219653;
		// font-weight: bold;
		font-family: "Sahitya", serif;
	}

	.fs-custom {
		font-size: 15px;
	}
}

//  --------------------------------------------------------------

.jumbotron {
	background-image: url("https://images.squarespace-cdn.com/content/v1/62f14aad574bf656d4fdcf67/73d5aa0f-6a29-4fa5-a8d2-8753e1af4e06/saffron-menu-62.jpg");

	background-position: top;
	background-size: cover;
	background-repeat: no-repeat;
	height: 15em;
	box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
}

.mask {
	text-shadow: 2px 2px 2px black;
}

.filterBox {
	background-color: rgba(165, 165, 165, 0.1);
	backdrop-filter: blur(15px);
	border-radius: 10px;
}

.btnColor {
	color: #219653;
	font-weight: bold;
	font-family: "Sahitya", serif;
}

.btnColor:hover {
	color: white;
	font-weight: bold;
	font-family: "Sahitya", serif;

	transition: all 0.4s ease-in-out;
	background-color: #219653;
	border-radius: 8px;
}

// -------------------------------------------------------------
// SECTION: ACCORDION CLASSES

.accordion {
	border: none;
}

.accordion-button:not(.collapsed) {
	color: var(--bs-accordion-active-color);
	background-color: white;
	box-shadow: inset 0 calc(-1* var(--bs-accordion-border-width)) 0 #ffffff;
}

.accordion-button:is(.collapsed) {
	color: var(--bs-accordion-active-color);
	background-color: white;
	box-shadow: inset 0 calc(-1* var(--bs-accordion-border-width)) 0 #ffffff;
}

.accordion-button[data-v-81550567]:not(.collapsed) {
	color: var(--forestGreen);
	background-color: white;
	box-shadow: inset 0 calc(-1* var(--bs-accordion-border-width)) 0 #ffffff;
}

.accordion-button[data-v-81550567]:is(.collapsed) {
	color: var(--forestGreen);
	background-color: white;
	box-shadow: inset 0 calc(-1* var(--bs-accordion-border-width)) 0 #ffffff;
}

.accordionContainer {
	// box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
	box-shadow: rgba(50, 50, 93, 0.25) 0px 2px 5px -1px, rgba(0, 0, 0, 0.3) 0px 1px 3px -1px;
}
</style>
