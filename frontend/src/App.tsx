import './App.css';
import Input from './components/Input/Input';
import Result from './components/Result/Result';

const App = () => {
    return (
        <main className="app justify-content-center vh-100 d-flex w-100 h-100 p-3 mx-auto flex-column">
            <Input />
            <Result />
        </main>
    );
}

export default App;
