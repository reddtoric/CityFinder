import React, { Component } from 'react';
import { withStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Map from './Map';

const styles = theme => ({
    textFieldShort: {
        margin: theme.spacing(1),
        width: '15ch',
    },
    textFieldLong: {
        margin: theme.spacing(1),
        width: '25ch',
    },
    button: {
        margin: theme.spacing(1),
    }
});


export default withStyles(styles)(
    class FetchData extends Component {
        static displayName = FetchData.name;

        constructor(props) {
            super(props);
            this.state = {
                location: null,
                loading: true,
                zipcode: '',
                country: 'US'
            };

            this.handleChangeZip = this.handleChangeZip.bind(this);
            this.handleChangeCountry = this.handleChangeCountry.bind(this);
            this.handleSubmit = this.handleSubmit.bind(this);
            this.handleClearInput = this.handleClearInput.bind(this);
        }

        handleChangeZip(event) {
            this.setState({ zipcode: event.target.value });
        }

        handleChangeCountry(event) {
            this.setState({ country: event.target.value });
        }

        handleSubmit(event) {
            event.preventDefault();
            this.populateData();
        }   

        handleClearInput(event) {
            this.setState({ zipcode: "", country: "" });
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

            let contents = this.state.loading
                ? <p><em>No search results found.</em></p> 
                : FetchData.renderTable(this.state.location);

            let map = this.state.loading
                ? <p><em>No search results for map.</em></p>
                : FetchData.renderMap(this.state.location);

            return (
                <div>
                    <h1 id="tableLabel" >City Finder</h1>
                    <form onSubmit={this.handleSubmit} autoComplete="off">
                        <div>
                            <TextField
                                className={classes.textFieldShort} 
                                required
                                id="outlined-required"
                                label="2-letter Country Code"
                                value={this.state.country}
                                onChange={this.handleChangeCountry}
                                variant="outlined"
                            />
                            <TextField
                                className={classes.textFieldLong} 
                                required
                                id="outlined-required"
                                label="Zip Code"
                                value={this.state.zipcode}
                                onChange={this.handleChangeZip}
                                variant="outlined"
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

        async populateData() {
            const response = await fetch('api/cityfinder?country=' + this.state.country + '&zipcode=' + this.state.zipcode);
            if (response.status === 200) {
                const data = await response.json();
                this.setState({ location: data, loading: false });
            }
            else {
                this.setState({ location: null, loading: true });
            }
        }
    }
)