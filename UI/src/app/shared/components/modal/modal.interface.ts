export enum ModalTypes{
    Error,
    Warning
}

export interface ModalInput {
    message: string;
    modalType: ModalTypes;
}