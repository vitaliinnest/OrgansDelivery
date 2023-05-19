import React from "react";
import {
    LineChart,
    Line,
    CartesianGrid,
    YAxis,
    XAxis,
    Tooltip,
    Label,
} from "recharts";

type DataUnit = {
    name: string;
    date: Date;
};

type Props = {
    data: DataUnit[];
};

const data = [
    {
        name: "Page A",
        uv: 400,
        pv: 2400,
        amt: 2400,
    },
    {
        name: "Page B",
        uv: 200,
        pv: 2400,
        amt: 2400,
    },
    {
        name: "Page C",
        uv: 300,
        pv: 2400,
        amt: 2400,
    },
];

const ConditionsLineChart = (props: Props) => {
    return (
        <LineChart
            width={600}
            height={300}
            data={data}
            margin={{ top: 5, right: 20, bottom: 5, left: 0 }}
        >
            <Line type="monotone" dataKey="uv" stroke="#8884d8" />
            <CartesianGrid stroke="#ccc" strokeDasharray="5 5" />
            <XAxis dataKey="name">
                <Label
                    value="Month"
                />
            </XAxis>
            <YAxis name="y axis">
                <Label
                    style={{ marginTop: "20px" }}
                    value="Value"
                    position="insideLeft"
                />
            </YAxis>
            <Tooltip />
        </LineChart>
    );
};

export default ConditionsLineChart;
