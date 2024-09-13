export default interface OrderByItem {
    PropertyName: string,
    Direction: string
}
export default interface OrderByModel {
    data: OrderByItem[]
}