import React, { Component } from 'react'
import { tableContext } from '../../App'
import PersonObject from '../person/PersonObject'
import PeopleTable from '../table/PeopleTable'

export default class Details extends Component {
	
	declare context: React.ContextType<typeof tableContext>
	
	render() {
		let person = this.context.data as PersonObject;
		return (
			<div><h2>Details for {person.name}</h2>
				<table className='table'>
					<thead><tr>
						<th>Property</th>
						<th>Value</th>
						<th>Action</th>
					</tr></thead>
					<tbody>
						{PeopleTable.columnMap.map(propName =>							
							<tr>
								<td>{propName}</td>
								<td>{person[propName]}</td>
								<td>
									<button 
										onClick={(event) => this.context.clickHandler(event)} 
										name="edit"
										type="button" 
										className="btn btn-warning">
											<i className="fa-solid fa-pen-to-square"></i>
									</button>
									<button 
										onClick={(event) => this.context.clickHandler(event)} 
										name="delete"
										type="button" 
										className="btn btn-danger">
											<i className="fa-solid fa-user-xmark"></i>
									</button>
								</td>
							</tr>
							)}
					</tbody>
				</table>
			</div>
			
		)
	}
}
