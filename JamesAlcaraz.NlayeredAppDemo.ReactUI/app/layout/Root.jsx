﻿import React from 'react';
import ReactDOM from 'react-dom';
import { Header } from './Header';
import { Footer } from './Footer';
import { SideMenu } from './SideMenu';

export class Root extends React.Component {
    render() {
        return(
            <div>
                <Header />
                
                <section id="main">
                    <SideMenu />

                    <section id="content">
                        <div className="container">

                            <div className="row">
                                <div className = "col-md-6">
                                    {this.props.children}
                                </div>
                            </div>

                        </div>
                    </section>
                </section>

                <Footer />

            </div>
            
        );
    }
}