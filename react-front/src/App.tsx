import React, { Component, MouseEvent } from 'react';
import Details from './components/editor/Details';
import PersonObject from './components/person/PersonObject';
import PeopleTable from './components/table/PeopleTable';

export const tableContext = React.createContext({
	data: {} as PersonObject | PersonObject[] | undefined,
	clickHandler: (any) => {}
});

export default class App extends Component<{}, {tableData: PersonObject[], loading: boolean, details?: PersonObject}> {
	static displayName = App.name;

	constructor(props) {
		super(props);
		this.state = { tableData: [], loading: true };
	}

	componentDidMount() {
		// console.log("didmountApp1");
		this.getPeopleList();
		// console.log("didmountApp2");
	}

	render() {
		// console.log("didrenderApp");
		if (this.state.details) {
			return (
				<div>
					<tableContext.Provider value={{ 
						data: this.state.tableData.find(row => row.id === this.state.details?.id), 
						clickHandler: this.detailsHandler }}>
						<Details />
					</tableContext.Provider>
				</div>
			);
		}
		else {
			return (
				<div>
					<tableContext.Provider value={{ data: this.state.tableData, clickHandler: this.selectRow }}>
						<h1>People!</h1>
						{this.state.loading
							? <p><em>Loading...</em></p>
							: <PeopleTable />}
					</tableContext.Provider>
				</div>
			);
		}
	}
	selectRow(rowKey){
		this.setState({details: rowKey})
	}
	detailsHandler(event:MouseEvent<HTMLElement>){
		if (event.currentTarget.getAttribute("name") === "delete") {
			this.deletePerson(this.state.details?.id);
		}
		
	}
	async getPeopleList() {
		const response = await fetch('api/People');
		const data = await response.json();
		this.setState({ tableData: data, loading: false });
	}
	async deletePerson(id) {
		const response = await fetch('api/People/DeletePerson/'+{id});

	}
}
