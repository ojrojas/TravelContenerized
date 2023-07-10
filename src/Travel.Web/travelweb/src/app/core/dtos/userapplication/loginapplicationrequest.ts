export interface ILoginApplicationRequest {
    grant_type: string | undefined;
    username: string;
    password: string;
    scope: string | undefined;
    client_id: string | undefined;
    client_secret: string | undefined;
}