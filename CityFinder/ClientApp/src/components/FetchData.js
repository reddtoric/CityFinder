import React, { Component } from 'react';
import { withStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Autocomplete from "@material-ui/lab/Autocomplete";
import Map from './Map';

const styles = theme => ({
    textField: {
        margin: theme.spacing(1),
        width: '25ch',
    },
    button: {
        margin: theme.spacing(1),
    },
    option: {
        fontSize: 15,
        "& > span": {
            marginRight: 10,
            fontSize: 18
        }
    }
});

const defaultCountry = {
    "alpha2Code": "US",
    "name": "United States of America"
};

// ISO 3166-1 alpha-2
// !!! No support for IE 11
function countryToFlag(isoCode) {
    return typeof String.fromCodePoint !== "undefined"
        ? isoCode
            .toUpperCase()
            .replace(/./g, (char) =>
                String.fromCodePoint(char.charCodeAt(0) + 127397)
            )
        : isoCode;
}

    class FetchData extends Component {
        static displayName = FetchData.name;

        constructor(props) {
            super(props);
            this.state = {
                isCityFound: false,
                locationObj: null,
                countries: [],
                zipcode: '',
                countryObj: defaultCountry,
                isCountryEmpty: false,
            };

            this.handleChangeZip = this.handleChangeZip.bind(this);
            this.handleChangeCountry = this.handleChangeCountry.bind(this);
            this.handleSubmit = this.handleSubmit.bind(this);
            this.handleClearInput = this.handleClearInput.bind(this);

            this.getCountries();
        }

        componentDidMount() {
            this.zipcodeInput.focus();
        }

        handleChangeZip(event) {
            this.setState({ zipcode: event.target.value });
        }
        
        handleChangeCountry(event, newValue) {
            this.setState({ countryObj: newValue, isCountryEmpty: newValue === null });
        }

        handleSubmit(event) {
            event.preventDefault();
            this.findCity();
        }

        handleClearInput(event) {
            this.setState({ zipcode: "", countryObj: null });
        }

        async findCity() {
            const response = await fetch('api/city?country=' + this.state.countryObj.alpha2Code + '&zipcode=' + this.state.zipcode);
            if (response.status === 200) {
                const data = await response.json();
                this.setState({ location: data, isCityFound: true });
            }
            else {
                this.setState({ location: null, isCityFound: false });
            }
        }

        async getCountries() {
            const response = await fetch('api/countries');
            if (response.ok) {
                const data = await response.json();
                this.setState({ countries: data, countriesLoading: false });
            }
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
                        <tr>
                            <td>{location.postal_code}</td>
                            <td>{location.city}</td>
                            <td>{location.country_code}</td>
                        </tr>
                    </tbody>
                </table>
            );
        }

        render() {
            const { classes } = this.props;

            let contents = !this.state.isCityFound
                ? <p><em>No search results found.</em></p>
                : FetchData.renderTable(this.state.location);

            let map = !this.state.isCityFound
                ? <p><em>No search results for map.</em></p>
                : FetchData.renderMap(this.state.location);

            return (
                <div>
                    <h1 id="tableLabel" >City Finder</h1>
                    <form onSubmit={this.handleSubmit} autoComplete="off">
                        <div>
                            <Autocomplete
                                autoHighlight
                                style={{ width: 400 }}
                                options={this.state.countries}
                                classes={{
                                    option: classes.option
                                }}
                                renderOption={(option) => (
                                    <React.Fragment>
                                        <span>{countryToFlag(option.alpha2Code)}</span>
                                        {option.name}
                                    </React.Fragment>
                                )}
                                renderInput={(params) => (
                                    <TextField
                                        {...params}
                                        required
                                        error={this.state.countryObj === null}
                                        label="Choose a country"
                                        variant="outlined"
                                        inputProps={{
                                            ...params.inputProps,
                                            autoComplete: "new-password"
                                        }}
                                    />
                                )}
                                getOptionLabel={(option) => `${option.alpha2Code}  ${option.name}`}
                                getOptionSelected={(option) => option.alpha2Code}
                                defaultValue={defaultCountry}
                                value={this.state.countryObj}
                                onChange={this.handleChangeCountry}
                            />

                            <TextField
                                inputRef={(input) => { this.zipcodeInput = input; }}
                                className={classes.textField}
                                required
                                error={this.state.zipcode === ''}
                                id="outlined-required"
                                label="Zip Code"
                                value={this.state.zipcode}
                                onChange={this.handleChangeZip}
                                variant="outlined"
                                inputProps={{
                                    maxLength: 10
                                }}
                            />
                        </div>
                        <div>
                            <Button
                                className={classes.button}
                                variant="contained"
                                onClick={this.handleClearInput}
                            >
                                Clear
                            </Button>
                            <Button
                                className={classes.button}
                                variant="contained"
                                color="primary"
                                type="submit"
                            >
                                Search
                            </Button>
                        </div>
                    </form>
                    {contents}
                    {map}
                </div>
            );
        }
}

export default withStyles(styles)(FetchData);