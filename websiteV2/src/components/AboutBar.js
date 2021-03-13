import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { withRouter } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import ButtonGroup from '@material-ui/core/ButtonGroup';

const styles = {
    sidebarContainer: {
        width: '20%',
        height: '100%',
        backgroundColor: '#424242',
        margin: '0px',
        display: 'flex',
        flexDirection: 'column',
        overflow: 'auto',
    },
    primaryButtons: {
        display: 'flex',
        justifyContent: 'left',
        color: '#F2F2F2',
        '&:hover': {
            color: '#33A1FD',
        },
        fontSize: '30px',
        fontFamily: 'Georgia',
    },
    buttons: {
        display: 'flex',
        justifyContent: 'center',
        color: '#F2F2F2',
        '&:hover': {
            color: '#33A1FD',
        },
        fontSize: '20px',
        fontFamily: 'Georgia',
    },
};

class AboutBar extends React.Component {
    constructor(props) {
        super(props);

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(event) {
        if (event.currentTarget.id === "about") {
            this.props.history.push("/");
        } else {
            this.props.history.push("/" + event.currentTarget.id);
        }
    }

    render() {
        return(
            <Paper elevation={2} square={true} className={this.props.classes.sidebarContainer}>
                <Button className={this.props.classes.primaryButtons} onClick={this.handleClick} id="about">About</Button>
                <ButtonGroup orientation="vertical">
                    <Button className={this.props.classes.buttons} onClick={this.handleClick} id="registry">Windows Registry</Button>
                    <Button className={this.props.classes.buttons} onClick={this.handleClick} id="shellbags">Shellbags</Button>
                </ButtonGroup>
                <Button className={this.props.classes.primaryButtons} onClick={this.handleClick} id="seeshells">SeeShells</Button>
                <ButtonGroup orientation="vertical">
                    <Button className={this.props.classes.buttons} onClick={this.handleClick} id="parser">Parsing</Button>
                    <Button className={this.props.classes.buttons} onClick={this.handleClick} id="analysis">Analysis</Button>
                    <Button className={this.props.classes.buttons} onClick={this.handleClick} id="timeline">Timeline</Button>
                    <Button className={this.props.classes.buttons} onClick={this.handleClick} id="filters">Filtering</Button>
                </ButtonGroup>
                <Button className={this.props.classes.primaryButtons} onClick={this.handleClick} id="case-studies">Case Studies</Button>
            </Paper>
        );
    }
}

export default withStyles(styles)(withRouter(AboutBar));