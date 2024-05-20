import { DatabaseItem } from "./DatabaseItem.js";

export class Recipe extends DatabaseItem {
  constructor(data) {
	super(data) //calls constructor on DataBaseItem

			// id, createdAt, updatedAt inherited from DatabaseItem

		this.title = data.title
		this.instructions = data.instructions
		this.img = data.img
		this.category = data.category
		this.creatorId = data.creatorId

		this.creator = data.creator
  }
}
