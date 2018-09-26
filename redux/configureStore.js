import {createStore, applyMiddleware} from 'redux';
import thunk from 'redux-thunk';
import reducers from './reducers';
import axios from 'axios';
import axiosMiddleware from 'redux-axios-middleware';

import { composeWithDevTools } from 'redux-devtools-extension';

///ENVIRONMENT VARIABLES
const production = process.env.NODE_ENV && process.env.NODE_ENV === "production";

if(!production) {
    require('dotenv').config();
}

const restUrl = production ? 
    process.env.PROD_RESTURL :
    process.env.JSONSERVER_RESTURL;
////////////////////////////////////////


export default function configureStore(initialState = {}) {

    //Setup middleware
    let middleware = [
        thunk,
        axiosMiddleware(axios.create({baseURL:restUrl}))
    ];

    //if not currently in production then push the redux immutable middleware to axios
    if(!production) {
        var mwInvariant = 
            require('redux-immutable-state-invariant')
            .default();
        middleware.push(mwInvariant);
    }

    const composeEnhancers = composeWithDevTools({});

    ///
    /// Setup the base url for the service based on the environment variables set abovee
    ///
    const client = axios.create({ //all axios can be used, shown in axios documentation
        baseURL: restUrl,
        //responseType: 'json'
    });

    return createStore(
        reducers,
        initialState,
        composeEnhancers(
            applyMiddleware(thunk, 
                axiosMiddleware(client))
        )
    );
}
