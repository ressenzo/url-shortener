import { useRef } from 'react';
import './Result.css';
import { Tooltip } from 'bootstrap';

interface ResultProps {
    shortenedUrl: string;
    isLoading: boolean;
}

const Result = ({ shortenedUrl = '', isLoading }: ResultProps) => {
    const buttonRef = useRef<HTMLButtonElement>(null);
    const tooltipRef = useRef<Tooltip | null>(null);

    const handleCopy = async () => {
        if (!buttonRef.current) return;
        
        buttonRef.current.disabled = true;
        tooltipRef.current?.dispose();

        tooltipRef.current = new Tooltip(buttonRef.current!, {
            title: "Copied",
            placement: "top",
            trigger: "manual"
        });

        tooltipRef.current.show()

        await navigator.clipboard.writeText(shortenedUrl);

        setTimeout(() => {
            if (tooltipRef.current) {
                tooltipRef.current.hide();
                tooltipRef.current = null;
            }

            if (buttonRef.current) {
                buttonRef.current.disabled = false;
            }
        }, 800)
    }

    return (
        <>
            {
                isLoading ?
                    <div className="text-center m-4">
                        <div className="spinner-border text-info" role="status">
                            <span className="visually-hidden">Loading...</span>
                        </div>
                    </div> :
                    null
            }
            
            <div className={`result mt-4 ${shortenedUrl === '' || isLoading ? "result-hidden" : undefined}`}>
                <p className="result-text">Your result</p>
                <div className="input-group">
                    <input
                        className="form-control"
                        type="text"
                        value={shortenedUrl}
                        aria-label="readonly input example"
                        readOnly={true}
                    />
                    <button
                        className="btn btn-info"
                        onClick={handleCopy}
                        ref={buttonRef}>
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="16"
                            height="16"
                            fill="currentColor"
                            className="bi bi-copy"
                            viewBox="0 0 16 16">
                            <path
                                fillRule="evenodd"
                                d="M4 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2zm2-1a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1zM2 5a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1v-1h1v1a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V6a2 2 0 0 1 2-2h1v1z"
                            />
                        </svg> Copy
                    </button>
                </div>
            </div>
        </>
    )
}

export default Result;
