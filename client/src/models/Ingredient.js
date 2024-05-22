import { DatabaseItem } from "./DatabaseItem.js";

export class Ingredient extends DatabaseItem {
  constructor(data) {
	super(data) //calls constructor on DataBaseItem

		// id, createdAt, updatedAt inherited from DatabaseItem

		this.name = data.name
		this.quantity = data.quantity
		this.recipeId = data.recipeId
  }
}
