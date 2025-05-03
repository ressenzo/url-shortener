import { useState } from 'react';
import './App.css';
import Input from './components/Input/Input';
import Result from './components/Result/Result';

const TITLE: string = "Url Shortener";
const App = () => {
    const [result, setResult] = useState<string>();


    return (
        <main className="app p-4 vh-100 d-flex w-100 h-100 mx-auto flex-column">
            <div className="app-title text-center d-none d-lg-block mb-3">
                <h1 className="display-3">{TITLE}</h1>
            </div>
            <div className="app-title text-center d-block d-lg-none">
                <h1 className="display-3">{TITLE}</h1>
            </div>
            <Input
                setResult={setResult}
            />
            <Result
                result={result!}
            />
        </main>
    );
}

export default App;
