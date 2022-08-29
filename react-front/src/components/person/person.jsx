import React, { Component } from 'react'

export default class Person extends Component {
	
	render() {
		return (
			<tr className='pRow' key={this.props.personData.id} onClick={(event) => this.props.clickHandler(event, this.props.key)}>
				<td className='pIdRow'>{this.props.personData.id}</td>
				<td className='pNameRow'>{this.props.personData.name}</td>
				<td className='pPhoneRow'>{this.props.personData.phonenumber}</td>
				<td className='pCityRow'>{this.props.personData.city}</td>
				<td className='pLangRow'><ul>{this.props.personData.languages.split(', ').map((l, i) => (<li key={i}>{l}</li>))}
				</ul></td>
			</tr>
		)
	}
}
