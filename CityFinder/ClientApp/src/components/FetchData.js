import React, { Component } from 'react';
import Map from './Map';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            location: null,
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
        event.preventDefault();
        this.populateData();
    }

    static renderMap(location) {
        return (
            <Map lat={parseFloat(location.latitude)} lng={parseFloat(location.longitude)} />
        );
    }

    static renderTable(location) {
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
                    <td>{location.zipCode}</td>
                    <td>{location.city == null ? "Not Found" : location.city}</td>
                    <td>{location.country}</td>
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>No searches results yet.</em></p> 
            : FetchData.renderTable(this.state.location);

        let map = this.state.loading
            ? <p><em>No searches results yet for map.</em></p>
            : FetchData.renderMap(this.state.location);

        return (
            <div>
                <h1 id="tabelLabel" >City Finder</h1>
                <p>The only country that will return a city with a valid zip code is "United States". (case sensitive)</p>
                <p>Limited 10 searches per hour. After limit, valid inputs will still return not found.</p>
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
                {map}
            </div>
        );
    }

    async populateData() {
        const response = await fetch('api/cityfinder?country=' + this.state.country + '&zipcode=' + this.state.zipcode);
        const data = await response.json();
        this.setState({ location: data, loading: false });
    }
}