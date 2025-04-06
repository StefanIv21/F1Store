export interface MetaData {
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
}

export class PaginatedResponse<T> {
    items: T;
    metadata: MetaData;

    constructor(items: T, metadata: MetaData) {
        this.items = items;
        this.metadata = metadata;
    }
}