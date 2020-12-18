import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor (props) {
    super(props);
    this.state = { forecasts: [], loading: true };

      fetch('api/SampleData/CovidList')
      .then(response => response.json())
      .then(data => {
        this.setState({ forecasts: data, loading: false });
      });
  }

  static renderForecastsTable (forecasts) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Country</th>
            <th>Position</th>
            <th>Atived</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.position}>
                  <td>{forecast.country}</td>
                  <td>{forecast.position}</td>
                  <td>{forecast.totalAtived}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1>TOP 10 COVID Countries</h1>
        {contents}
      </div>
    );
  }
}
