/* eslint-disable no-useless-computed-key */
// import React from 'react'

// export default function TableHead(columns, table) {

// 	return (
// 		<thead>
// 			<tr>
// 			{columns.map(col => 
// 				<th onClick={table.columnClick} className={col[0]}>{col[1]}</th>
// 				)}
// 			</tr>
// 		</thead>
// 	)
// }

import React, { Component } from 'react'

export default class TableHead extends Component {
	
	static sortSymbol = { 
		[-1]: <i className="fa-solid fa-sort-up"></i>, 
		[0]: <i className="fa-solid fa-sort"></i>, 
		["x"]: <i className="fa-solid fa-sort transparent"></i>, 
		[1]: <i className="fa-solid fa-sort-down"></i> }
	
	// componentDidMount() {
	// 	console.log("didmount");
	// 	this.columnClick = this.columnClick.bind(this);
	// 	// this.isSorted = this.isSorted.bind(this);
	// }
	render() {
		return (
			<thead className='thead'>
				<tr>
					{this.props.columnNames.map((col, index) =>
						<th key={col[0]}
							onClick={() => this.columnClick(index)}
							className="colHead ">
							{this.isSorted(index)}{col[1]}
						</th>
					)}
				</tr>
			</thead>
		);
	}
	columnClick(index){
		// let sorting = this.state.sorting;
		// // eslint-disable-next-line react/no-direct-mutation-state
		// this.state.sorting = 
		// 	sorting === 0 ? index : 
		// 	sorting === index ? sorting*-1 : 
		// 	Math.abs(sorting) !== index ? index : 0;
		
		this.clickHandler(index);
	}
	isSorted(index) {
		let sorting = this.props.sorting;

		return sorting === 0 ? TableHead.sortSymbol[0]
				: Math.abs(sorting) === index ? TableHead.sortSymbol[sorting / index]
					: TableHead.sortSymbol["x"];
	}
}

// 					{this.isSorted(this.key)}{col[1]}
// 			</th>
// 		);
		
// 		this.state = { colHeads, sorting: 0 };
// 	}
// 	render() {
// 		return (
// 			<thead>
// 				<tr>
// 					{this.state.colHeads.map(col => this.isSorted(col))}
// 				</tr>
// 			</thead>
// 		);
// 	}
// 	columnClick(ev, key){
		
// 		this.setState((sorting) => sorting = sorting === 0 ? key : sorting === key ? sorting*=-1 : sorting = 0 );
// 	}
// 	isSorted(col) {
// 		let sorting = this.state.sorting;

// 		let sortIcon =
// 			sorting === 0 ? TableHead.sortSymbol[0]
// 				: Math.abs(sorting) === col.key ? TableHead.sortSymbol[sorting / col]
// 					: " ";

// 		cloneElement(col, sortIcon)
// 		col.props.innerText = `<i class="fa-solid ${sortIcon}"></i>${col.props.innerText}`;
// 		return col;
// 	}

// }