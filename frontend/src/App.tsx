import './App.css';
import Input from './components/Input/Input';
import Result from './components/Result/Result';

const App = () => {
    return (
        <main className="app justify-content-center vh-100 d-flex w-100 h-100 mx-auto flex-column">
            <div className="app-title text-center">
                <h1 className="display-3">Url Shortener</h1>
            </div>
            <Input />
            <Result
                result=""
            />
        </main>
    );
}

export default App;
