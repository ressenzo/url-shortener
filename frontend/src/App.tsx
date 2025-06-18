import { useState } from 'react';
import './App.css';
import Input from './components/Input/Input';
import Result from './components/Result/Result';

const TITLE: string = "Url Shortener";
const App = () => {

    const [shortenedUrl, setShortenedUrl] = useState<string>('');
    const [isLoading, setIsloading] = useState<boolean>(false);

    return (
        <main className="app p-4 vh-100 d-flex w-100 h-100 mx-auto flex-column">
            <div className="app-title text-center d-none d-lg-block mb-3">
                <h1 className="display-3">{TITLE}</h1>
            </div>
            <div className="app-title text-center d-block d-lg-none">
                <h1 className="display-3">{TITLE}</h1>
            </div>
            <Input
                setShortenedUrl={setShortenedUrl}
                setIsLoading={setIsloading}
            />
            <Result
                shortenedUrl={shortenedUrl!}
                isLoading={isLoading}
            />
        </main>
    );
}

export default App;
