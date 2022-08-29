import React, { Component } from 'react'
import { tableContext } from '../../App';
import Person from '../person/person';
import PersonObject from '../person/PersonObject';
import TableHead from './TableHead';

export default class PeopleTable extends Component<{}, {sorting: number}> {
	static contextType = tableContext;
	static peopleTableHead = [
		["pIdHead", "ID"],
		["pNameHead", "Full name"],
		["pPhoneHead", "Phone number"],
		["pCityHead", "City of Residence"],
		["pLangHead", "Spoken Languages"]
	];
	static columnMap = [
		"id",
		"name",
		"phonenumber",
		"city",
		"languages"
	];

	declare context: React.ContextType<typeof tableContext>
	constructor(args) {
		super(args);
		this.state = { 
			sorting: 0 
		};
	}
	componentDidMount() {
		// console.log("didmount");
		this.columnClick = this.columnClick.bind(this);
	}
	componentWillUnmount(){
		// console.log("willUNmount");
	}
	render() {
		// console.log("didrender");
		return (
			<table className='table table-striped' aria-labelledby="tabelLabel">
				{<TableHead columnNames={PeopleTable.peopleTableHead} clickHandler={this.columnClick} sorting={this.state.sorting} />}
				<tbody>
					{this.sortRows(this.context.data as PersonObject[]).map(personData => <Person personData={personData} key={personData["id"]} clickHandler={this.context.clickHandler} />)}
				</tbody>
			</table>
		);
	}

	sortRows(people: PersonObject[]){
	
		let colIndex = Math.abs(this.state.sorting);
		let order = people.sort(compareRows);
		
		if (this.state.sorting < 0) {
			order = order.reverse();
		}
		function compareRows(a, b): 1 | -1 | 0 {
			let key = PeopleTable.columnMap[colIndex];
			return a[key]  < b[key] ? -1 : a[key] > b[key] ? 1 : 0;
		}

		return order;
	}
	columnClick(colIndex){

		let sorting = this.state.sorting;

		let newSorting = 
			sorting === 0 ? colIndex : 
			sorting === colIndex ? sorting*-1 : 
			Math.abs(sorting) !== colIndex ? colIndex : 0;
		
		this.setState({sorting : newSorting});
	}
	rowClick(rowKey) {
		
	}
}

PeopleTable.contextType = tableContext;