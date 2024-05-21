import { DatabaseItem } from "./DatabaseItem.js";

export class Favorite extends DatabaseItem {
  constructor(data) {
	super(data) //calls constructor on DataBaseItem

		// id, createdAt, updatedAt inherited from DatabaseItem

		this.recipeId = data.recipeId
		this.accountId = data.accountId
  }
}
