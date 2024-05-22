import { DatabaseItem } from "./DatabaseItem.js";

export class Favorite extends DatabaseItem {
  constructor(data) {
	super(data) //calls constructor on DataBaseItem

		// id, createdAt, updatedAt inherited from DatabaseItem
		this.favoriteId = data.favoriteId
		this.recipeId = data.id
		this.accountId = data.accountId
  }
}
