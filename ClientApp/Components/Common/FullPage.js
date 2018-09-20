import React, {Component} from 'react';

import CodeCampMenu from './CodeCampMenu';
import PageTop from './PageTop';
import Footer from './Footer';
import Routes from "../../Routes";

class FullPage extends Component {
    render() {
        return (
            <div>
                <PageTop>
                    <CodeCampMenu />
                </PageTop>
                <Routes />
                <Footer />
            </div>
        );
    }
}

FullPage.defaultProps = {};

export default FullPage;
