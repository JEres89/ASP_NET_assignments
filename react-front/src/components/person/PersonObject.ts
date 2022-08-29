export default class PersonObject {
	id: number;
	name: string;
	phonenumber: string;
	city: string;
	languages: string[];

	constructor(data){
		({id: this.id, name: this.name, phonenumber: this.phonenumber, city: this.city, languages: this.languages} = data);
	}
}
