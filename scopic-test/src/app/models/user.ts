import { Role } from './role';

export class User {
    userId: string;
    username: string;
    password: string;
    role: Role;
    token?: string;
}