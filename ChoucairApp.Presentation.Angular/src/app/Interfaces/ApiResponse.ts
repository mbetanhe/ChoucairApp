export interface ApiResponse<Type>{
    data:	Type,
    messages:	string[],
    succeeded:	boolean
}