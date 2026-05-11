import type { Order } from "../../types/order";
import "./styles.css";

interface Props {
  order: Order;
}

export function OrderCard({ order }: Props) {
  return (
    <div className="order-card">
      <h3>{order.customer}</h3>

      <p>
        Valor:
        <strong> R$ {order.value}</strong>
      </p>

      <p>Data: {new Date(order.orderDate).toLocaleString()}</p>

      <small>{order.id}</small>
    </div>
  );
}
