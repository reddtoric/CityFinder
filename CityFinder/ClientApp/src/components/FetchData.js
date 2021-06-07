import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            locations: [],
            loading: true,
            zipcode: '',
            country: 'United States'
        };

        this.handleZipChange = this.handleZipChange.bind(this);
        this.handleCountryChange = this.handleCountryChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleZipChange(event) {
        this.setState({ zipcode: event.target.value });
    }

    handleCountryChange(event) {
        this.setState({ country: event.target.value });
    }

    handleSubmit(event) {
        //alert('Searching city in ' + this.state.country + ' with zip code ' + this.state.zipcode);
        event.preventDefault();
        this.populateData();
    }

    static renderTable(locations) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Zip Code</th>
                        <th>City</th>
                        <th>Country</th>
                    </tr>
                </thead>
                <tbody>
                    {locations.map((location, index) =>
                        <tr key={index} >
                            <td>{location.zipCode}</td>
                            <td>{location.isFound ? location.city : "Not Found"}</td>
                            <td>{location.country}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>No searches results yet.</em></p> 
            : FetchData.renderTable(this.state.locations);

        return (
            <div>
                <h1 id="tabelLabel" >City Finder</h1>
                <p>The only country that will return a city with a valid zip code is "United States".</p>
                <p>Limited 10 searches per hour.</p>
                <form onSubmit={this.handleSubmit} >
                    <label>
                        Country:
                        <input type="text" name="country" value={this.state.country} onChange={this.handleCountryChange} />
                    </label>
                    <br />
                    <label>
                        Zip Code:
                    <input type="text" name="zipcode" value={this.state.zipcode} onChange={this.handleZipChange} />
                    </label>
                    <br />
                    <input type="submit" value="Search" />
                </form>
                {contents}
            </div>
        );
    }

    async populateData() {
        const response = await fetch('api/cityfinder?country=' + this.state.country + '&zipcode=' + this.state.zipcode); /// same as '...api/cityfinder'
        const data = await response.json();
        this.setState({ locations: [data,...this.state.locations], loading: false });
    }
}