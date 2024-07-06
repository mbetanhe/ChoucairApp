export interface AuthenticationResponse{
    message:	string,
    isAuthenticated:	boolean,
    userName:	string,
    id: string,
    email:	string
    roles:	string[]
    token:	string
}