
export interface ElmahErrorList {
    total: number;
    pages: number;
    currentPage: number;
    errors: ElmahError[];
}

export interface ElmahError {

    errorId: string;
    application: string;
    host: string;
    type: string;
    source: string;
    message: string;
    user: string;
    statusCode: number;
    timeUtc: Date;
    allXml: string;
}