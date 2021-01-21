import MenuBar from './components/MenuBar.js';
import FrontPage from './containers/FrontPage.js';
import { withStyles } from '@material-ui/core/styles';

const styles = {
  root: {
    color: 'white',
    display: 'flex',
    margin: '0px',
  },
  content: {
    color: 'black',
  },
};

function App(props) {
  return (
    <div>
      <div className={props.classes.root}>
        <MenuBar/>
      </div>
      <div className={props.classes.content}>
        <FrontPage/>
      </div>
    </div>
  );
}

export default withStyles(styles)(App);